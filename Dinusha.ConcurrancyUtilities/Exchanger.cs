using System;

namespace Dinusha.ConcurrancyUtilities
{
	/// <summary>
	/// Exchanger is used to exchange objects between two threads 
	/// </summary>
	public class Exchanger <T>
	{
		private int threadcount = 0;
		private Semaphore putPermition;
		private Semaphore takePermition;
		private Semaphore lockExternals;
		private object lockobject;
		private T temp;

		/// <summary>
		/// Initializes a new instance of the <see cref="Dinusha.ConcurrancyUtilities.Exchanger`1"/> class.
		/// </summary>
		public Exchanger ()
		{

			lockobject = new object ();
			/// permition to put to the temp object 
			putPermition = new Semaphore (1);

			///permition to take from the temp object 
			takePermition = new Semaphore (0);

			//Allows only two threads to exchange
			lockExternals = new Semaphore (2);



		}

		/// <summary>
		/// Exchange the specified myobject.
		/// </summary>
		/// <param name="myobject">Myobject.</param>
		public T Exchange (T myobject)
		{
			//temporary variable
			T rvalue;

			//To keep that one thread has arrive.
			lockExternals.Acquire ();

			lock (lockobject) {
				//Thread count 
				threadcount++;
			}
			//Provides permition to put an object to the temp variable 
			//if there are two thread already exchanging, it waits 
			putPermition.Acquire ();
		
			// When only 1 thread arives, the object is copied to the temp.
			if (threadcount == 1) {

			
				temp = myobject;
				/// Another thread can arrive 
				putPermition.Release ();

				/// 
				takePermition.Acquire ();

				rvalue = temp;

				lockExternals.Release (2);


				// when two thereads arrive then 
			} else if (threadcount == 2) {

				rvalue = temp;

				temp = myobject;

				takePermition.Release ();

				putPermition.Release ();


			} else {
				///When more than two threads are tring to exchange 
				throw new InvalidOperationException ("Cannot exchange more than 2 threads ");
			
			}

			lock (lockobject) {
				threadcount--;
			}

			return rvalue;

		}
	}
}

