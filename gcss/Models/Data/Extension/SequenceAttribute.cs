using System;

namespace gcss.Models.Data.Extension
{

	public class SequenceAttribute : Attribute
	{
		public string SequenceName { get; set; }
		public string FieldName { get; set; }

		public SequenceAttribute(string fieldName, string sequenceName)
		{
			FieldName = fieldName;
			SequenceName = sequenceName;
		}

	}

}