using System;

namespace Model
{
	/// <summary>
	/// A class that represents a simple data transfer object
	/// </summary>
	public class SimpleMessage
	{
		/// <summary>
		/// The text of the message
		/// </summary>
		public string Text { get; set; }

		/// <summary>
		/// A message type. The message type determines how 
		/// the consumer app processes the message
		/// </summary>
		public SimpleMessageType MessageType { get; set; }
	}
}
