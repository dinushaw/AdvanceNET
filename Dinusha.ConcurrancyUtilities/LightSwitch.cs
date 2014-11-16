using System;

namespace Dinusha.ConcurrancyUtilities
{
	/// <summary>
	/// Light switch is used to provide permition for a group of threds 
	/// </summary>
	public class LightSwitch
	{
		readonly Semaphore takepermition;
		int count= 0;

		public LightSwitch (Semaphore s1)
		{
			takepermition = s1;
		}

		/// <summary>
		/// Aquires permition for the whole group to proceed 
		/// </summary>
		public void Acquire(){
			lock (this) {
				if (count == 0) {
					takepermition.Acquire ();
				}
				count++;
			}

		}
		/// <summary>
		/// releses aquired group permition.
		/// </summary>
		public void Release(){
			lock (this) {
				count--;
				if (count == 0) {
					takepermition.Release ();
				}
			}
				
		}
	}
}

