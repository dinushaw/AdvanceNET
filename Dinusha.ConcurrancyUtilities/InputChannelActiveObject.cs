using System;


namespace Dinusha.ConcurrancyUtilities
{
	abstract class InputChannelActiveObject<IT>:ActiveObject
	{
		/// <summary>
		/// The input channel.
		/// </summary>
		readonly Channel<IT> inputChannel = new Channel<IT>();

		public  InputChannelActiveObject ():base() {}

		/// <summary>
		/// Takes data and inserts into the Channel 
		/// </summary>
		protected override void Execute(){

			while (true) {
				Process (inputChannel.Take());
			}
		}

		/// <summary>
		/// Process the specified data.
		/// </summary>
		/// <param name="data">Data.</param>
		abstract protected void Process(IT data);

	}
}

