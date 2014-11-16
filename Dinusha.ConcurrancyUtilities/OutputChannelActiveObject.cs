using System;

namespace Dinusha.ConcurrancyUtilities
{/// <summary>
/// Output channel active object.
/// </summary>
	abstract class OutputChannelActiveObject<OT> :ActiveObject
	{
		public readonly Channel<OT> outputChannel = new Channel<OT>();

			public OutputChannelActiveObject (): base(){}
		/// <summary>
		/// Execute this instance.
		/// </summary>
		public override void Execute(){

			while (true) {
				Process (outputChannel.Take ());
			}
		}

		protected void Process(OT data);
	}
}

