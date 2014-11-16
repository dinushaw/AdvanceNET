using System;
using System.Threading;

namespace Dinusha.ConcurrancyUtilities
{
	/// <summary>
	/// Utility that restrict access according to the number of tokens availabe.
	/// No of tokens cannot be negative 
	/// </summary>
	/// 
	public class Semaphore
	{

		private UInt32 tokens = 0;
		protected readonly Object lockobject = new Object ();

		/// <summary>
		/// Constructor for Semaphore, Num of tokens cannot be a negative. Throws Argument Exception. 
		/// </summary>
		/// <param name="numTokens">Number tokens.No negative values</param>
		public Semaphore (int numTokens)
		{
			if (numTokens < 0) {
				throw new ArgumentException ("Invalid argument, enter positive value for no of tokens to release.");
			}
			tokens = (UInt32)numTokens;
		
		}

		/// <summary>
		/// Default constuctor, assigns 0 to the no of tokens. 
		/// </summary>
		public Semaphore ()
		{
			tokens =0;	
		
		}

		/// <summary>
		///Blocks threads until the tokens are avilable.if not threads will wait till a pulseAll command is being called. 
		///Functionality : check whether tokens are available and decrement the value of tokens, if not wait till a token is available.
		/// </summary>
		public virtual void Acquire ()
		{

			lock (lockobject) {
				while (tokens == 0) {
					Monitor.Wait (lockobject);
				}
				tokens--;
			}

		}

		/// <summary>
		/// 
		/// Release a number of tokens. increments the token count by 1 and PulseAll and the waiting threads. Throws argument exception for negative n. 
		/// Default value for no of tokensToRelease is 1  
		/// </summary>
		public virtual void Release (int tokensToRelase = 1)
		{
			if (tokensToRelase < 0) {
				throw new ArgumentException ("Invalid argument, enter positive value for no of tokens to release.");
			}
			lock (lockobject) {
				tokens += (UInt32)tokensToRelase;
				Monitor.PulseAll (lockobject);
			}
		}
	}
}

