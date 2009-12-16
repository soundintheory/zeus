﻿using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Zeus.BaseLibrary.ExtensionMethods
{
	public static class StringExtensionMethods
	{
		public static bool Contains(this string thisString, string value, StringComparison comparisonType)
		{
			return (thisString.IndexOf(value, comparisonType) != -1);
		}

		public static T Deserialize<T>(this string serializedValue)
		{
			return (T)Deserialize(serializedValue, typeof(T));
		}

		public static object Deserialize(this string serializedValue, Type type)
		{
			byte[] serialisedBytes = Convert.FromBase64String(serializedValue);
			using (MemoryStream stream = new MemoryStream(serialisedBytes))
			{
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				return binaryFormatter.Deserialize(stream);
			}
		}

		public static string FromBase64String(this string value)
		{
			ASCIIEncoding encoding = new ASCIIEncoding();
			Decoder decoder = encoding.GetDecoder();
			byte[] toDecodeBytes = Convert.FromBase64String(value);
			int charCount = decoder.GetCharCount(toDecodeBytes, 0, toDecodeBytes.Length);
			char[] decodedChars = new char[charCount];
			decoder.GetChars(toDecodeBytes, 0, toDecodeBytes.Length, decodedChars, 0);
			string result = new string(decodedChars);
			return result;
		}

		public static T FromXml<T>(this string xml)
		{
			StringReader stringReader = new StringReader(xml);

			XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
			T result = (T)xmlSerializer.Deserialize(stringReader);

			stringReader.Close();

			return result;
		}

		public static string Left(this string value, int length)
		{
			return (length >= value.Length) ? value : value.Substring(0, length);
		}

		public static string Paragraph(this string value, int paragraphIndex)
		{
			string[] paragraphs = value.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
			return paragraphs.Length > paragraphIndex ? paragraphs[paragraphIndex] : string.Empty;
		}

		public static string Paragraphs(this string value, int paragraphIndex)
		{
			string[] paragraphs = value.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
			if (paragraphs.Length > paragraphIndex)
				return string.Join(Environment.NewLine, paragraphs, paragraphIndex, paragraphs.Length - paragraphIndex);
			else
				return string.Empty;
		}

		public static string Right(this string value, int length)
		{
			return (length >= value.Length) ? value : value.Substring(value.Length - length, length);
		}

		public static string RemoveHtmlTags(this string value)
		{
			const string expression = "<.*?>";
			return Regex.Replace(value, expression, string.Empty);
		}

		public static string RemoveWrappingHtmlTags(this string value)
		{
			return Regex.Replace(value, @"^<.*?>(.*)<.*?>?", "$1");
		}

		public static string ToBase64String(this string value)
		{
			byte[] encodedDataBytes = Encoding.ASCII.GetBytes(value);
			string encodedData = Convert.ToBase64String(encodedDataBytes);
			return encodedData;
		}

		public static string ToPascalCase(this string value)
		{
			if (string.IsNullOrEmpty(value))
				return value;

			return char.ToLower(value[0]) + value.Substring(1);
		}

		public static string ToSafeUrl(this string value)
		{
			if (string.IsNullOrEmpty(value))
				return string.Empty;

			string result = value.Replace(' ', '-').ToLower();
			result = Regex.Replace(result, "[^a-zA-Z0-9\\-]", string.Empty);
			return result;
		}

		public static T ToEnum<T>(this string value)
		{
			return (T)Enum.Parse(typeof(T), value);
		}

		/// <summary>Gets the type from a string.</summary>
		/// <param name="typeName">The type name string.</param>
		/// <returns>The type.</returns>
		public static Type ToType(this string typeName)
		{
			if (typeName == null)
				throw new ArgumentNullException("name");

			Type t = Type.GetType(typeName);
			if (t == null)
				throw new Exception(string.Format("Couldn't find any type with the name '{0}'", typeName));

			return t;
		}

		public static string Truncate(this string value, int length)
		{
			return Truncate(value, length, true);
		}

		public static string Truncate(this string value, int length, bool preserveWordBoundaries)
		{
			if (string.IsNullOrEmpty(value))
				return value;

			if (value.Length <= length)
				return value;

			string[] words = value.Split(' ');
			int currentLength = 0;
			StringBuilder sb = new StringBuilder();
			for (int i = 0, arrayLength = words.Length; i < arrayLength && currentLength < length - 3; ++i)
			{
				string newEntry = words[i] + " ";
				sb.Append(newEntry);
				currentLength += newEntry.Length;
			}

			return sb.ToString() + "...";
		}

		public static int LuhnChecksum(this string value)
		{
			return value
			       	.Where(Char.IsDigit)
			       	.Reverse()
			       	.SelectMany((c, i) => ((c - '0') << (i & 1)).ToString())
			       	.Sum(c => c - '0') % 10;
		}

		/// <summary>
		/// Returns a string containing every character within a string before the 
		/// first occurrence of another string.
		/// </summary>
		/// <param name="str">Required. String expression from which the leftmost characters are returned.</param>
		/// <param name="search">The string where the beginning of it marks the 
		/// characters to return.  If the string is not found, the whole string is 
		/// returned.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException">Thrown if str or searchstring is null.</exception>
		public static string LeftBefore(this string str, string search)
		{
			return LeftBefore(str, search, StringComparison.InvariantCulture);
		}

		/// <summary>
		/// Returns a string containing every character within a string before the 
		/// first occurrence of another string.
		/// </summary>
		/// <param name="original">Required. String expression from which the leftmost characters are returned.</param>
		/// <param name="search">The string where the beginning of it marks the 
		/// characters to return.  If the string is not found, the whole string is 
		/// returned.</param>
		/// <param name="comparisonType">Determines whether or not to use case sensitive search.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException">Thrown if str or searchstring is null.</exception>
		public static string LeftBefore(this string original, string search, StringComparison comparisonType)
		{
			if (original == null)
				throw new ArgumentNullException("original", "The original string may not be null.");

			if (search == null)
				throw new ArgumentNullException("search", "Search string may not be null.");

			//Shortcut.
			if (search.Length > original.Length || search.Length == 0)
				return original;

			int searchIndex = original.IndexOf(search, 0, comparisonType);

			if (searchIndex < 0)
				return original;

			return Left(original, searchIndex);
		}

		/// <summary>
		/// Returns a string containing every character within a string after the 
		/// first occurrence of another string.
		/// </summary>
		/// <param name="original">Required. String expression from which the rightmost characters are returned.</param>
		/// <param name="search">The string where the end of it marks the 
		/// characters to return.  If the string is not found, the whole string is 
		/// returned.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException">Thrown if str or searchstring is null.</exception>
		public static string RightAfter(this string original, string search)
		{
			return RightAfter(original, search, StringComparison.InvariantCulture);
		}

		/// <summary>
		/// Returns a string containing every character within a string after the 
		/// first occurrence of another string.
		/// </summary>
		/// <param name="original">Required. String expression from which the rightmost characters are returned.</param>
		/// <param name="search">The string where the end of it marks the 
		/// characters to return.  If the string is not found, the whole string is 
		/// returned.</param>
		/// <param name="comparisonType">Determines whether or not to use case sensitive search.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException">Thrown if str or searchstring is null.</exception>
		public static string RightAfter(this string original, string search, StringComparison comparisonType)
		{
			if (original == null)
				throw new ArgumentNullException("original", "The original string may not be null.");
			if (search == null)
				throw new ArgumentNullException("search", "The searchString string may not be null.");

			//Shortcut.
			if (search.Length > original.Length || search.Length == 0)
				return original;

			int searchIndex = original.IndexOf(search, 0, comparisonType);

			if (searchIndex < 0)
				return original;

			return Right(original, original.Length - (searchIndex + search.Length));
		}

		/// <summary>
		/// Returns a string containing every character within a string after the 
		/// last occurrence of another string.
		/// </summary>
		/// <param name="original">Required. String expression from which the rightmost characters are returned.</param>
		/// <param name="search">The string where the end of it marks the 
		/// characters to return.  If the string is not found, the whole string is 
		/// returned.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException">Thrown if str or searchstring is null.</exception>
		public static string RightAfterLast(this string original, string search)
		{
			return RightAfterLast(original, search, original.Length - 1, StringComparison.InvariantCulture);
		}

		/// <summary>
		/// Returns a string containing every character within a string after the
		/// last occurrence of another string.
		/// </summary>
		/// <param name="original">Required. String expression from which the rightmost characters are returned.</param>
		/// <param name="search">The string where the end of it marks the
		/// characters to return.  If the string is not found, the whole string is
		/// returned.</param>
		/// <param name="startIndex">The start index.</param>
		/// <param name="comparisonType">Determines whether or not to use case sensitive search.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException">Thrown if str or searchstring is null.</exception>
		public static string RightAfterLast(this string original, string search, int startIndex, StringComparison comparisonType)
		{
			if (original == null)
				throw new ArgumentNullException("original", "The original string may not be null.");
			if (search == null)
				throw new ArgumentNullException("search", "The searchString string may not be null.");

			//Shortcut.
			if (search.Length > original.Length || search.Length == 0)
				return original;

			int searchIndex = original.LastIndexOf(search, startIndex, comparisonType);

			if (searchIndex < 0)
				return original;

			return Right(original, original.Length - (searchIndex + search.Length));
		}
	}
}