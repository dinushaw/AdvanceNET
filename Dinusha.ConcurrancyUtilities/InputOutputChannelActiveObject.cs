using System;

namespace Dinusha.ConcurrancyUtilities
{
	/// <summary>
	/// Input output channel active object.
	/// </summary>
	abstract class InputOutputChannelActiveObject<IT ,OT>:ActiveObject
	{
		public Channel<IT> inputChannel = new Channel<IT>();
		public Channel<OT> OutputChannel = new Channel<OT>();

		/// <summary>
		/// Initializes a new instance of the <see cref="Dinusha.ConcurrancyUtilities.InputOutputChannelActiveObject`2"/> class.
		/// </summary>
		public InputOutputChannelActiveObject (): base()
		{
		}

		/// <summary>
		/// Loops forever , takes parameter from input channel and places in the output channel.
		/// </summary>
		private override void Execute(){

			while (true) {
				OutputChannel.Put (Process (inputChannel.Take ()));
			}
		}

		/// <summary>
		/// Process the specified data.
		/// </summary>
		/// <param name="data">Data.</param>
		protected OT Process(IT data);
	}
}

