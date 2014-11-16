using System;

namespace Dinusha.ConcurrancyUtilities
{
	/// <summary>
	/// Mutex is inherited from the semaphore 
	/// </summary>
	public class Mutex:Semaphore
	{
		/// <summary>
		/// Mutex can have 0 or 1 tokens 
		/// </summary>
		public long tokens;

		/// <summary>
		/// Initializes a new instance Mutex class. Number of tokens are given as a  
		/// </summary>
		/// <param name="numTokens">Number tokens.</param>
		public Mutex (int numTokens=0)
		{
			if (numTokens <= 1) {
				tokens = numTokens;
			}
		}
		/// <summary>
		/// Acquire the main 
		/// </summary>
		public override void Acquire(){

			base.Acquire ();
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="a">The alpha component.</param>
		public override void Release(int a = 1){
			lock (this) {
				if (tokens == 0 && a == 1) {
					base.Release (1);
				} else {
					throw new ArgumentOutOfRangeException ();
				}
			}
		}
	}
}

