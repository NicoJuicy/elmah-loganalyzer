﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic.FileIO;

namespace Crepido.ElmahOfflineViewer.Core.Domain
{
	using Crepido.ElmahOfflineViewer.Core.Common;

	public class CsvParser
	{
		public IEnumerable<KeyValuePair<Uri, DateTime>> Parse(string content)
		{
			var bytes = Encoding.Unicode.GetBytes(content);
			using (var stream = new MemoryStream(bytes))
			using (var parser = new TextFieldParser(stream, Encoding.Unicode) { TextFieldType = FieldType.Delimited })
			{
				parser.SetDelimiters(",");
				foreach (var currentRow in parser.Read().Skip(/* headers */ 1))
				{
					var date = Convert.ToDateTime(currentRow[2]);
					var detailsUrl = new Uri(currentRow[9]);
					yield return new KeyValuePair<Uri, DateTime>(detailsUrl, date);
				}
			}
		}
	}
}
