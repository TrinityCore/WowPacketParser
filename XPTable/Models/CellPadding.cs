/*
 * Copyright © 2005, Mathew Hall
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without modification, 
 * are permitted provided that the following conditions are met:
 *
 *    - Redistributions of source code must retain the above copyright notice, 
 *      this list of conditions and the following disclaimer.
 * 
 *    - Redistributions in binary form must reproduce the above copyright notice, 
 *      this list of conditions and the following disclaimer in the documentation 
 *      and/or other materials provided with the distribution.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
 * ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
 * IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
 * INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
 * NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, 
 * OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
 * WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
 * ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY 
 * OF SUCH DAMAGE.
 */


using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;


namespace XPTable.Models
{
	#region CellPadding

	/// <summary>
	/// Specifies the amount of space between the border and any contained 
	/// items along each edge of an object
	/// </summary>
	[Serializable(), 
	StructLayout(LayoutKind.Sequential), 
	TypeConverter(typeof(CellPaddingConverter))]
	public struct CellPadding
	{
		#region Class Data
		
		/// <summary>
		/// Represents a Padding structure with its properties 
		/// left uninitialized
		/// </summary>
		public static readonly CellPadding Empty = new CellPadding(0, 0, 0, 0);
		
		/// <summary>
		/// The width of the left padding
		/// </summary>
		private int left;
		
		/// <summary>
		/// The width of the right padding
		/// </summary>
		private int right;
		
		/// <summary>
		/// The width of the top padding
		/// </summary>
		private int top;
		
		/// <summary>
		/// The width of the bottom padding
		/// </summary>
		private int bottom;

		#endregion


		#region Constructor

		/// <summary>
		/// Initializes a new instance of the Padding class
		/// </summary>
		/// <param name="left">The width of the left padding value</param>
		/// <param name="top">The height of top padding value</param>
		/// <param name="right">The width of the right padding value</param>
		/// <param name="bottom">The height of bottom padding value</param>
		public CellPadding(int left, int top, int right, int bottom)
		{
			this.left = left;
			this.right = right;
			this.top = top;
			this.bottom = bottom;
		}

		#endregion


		#region Methods

		/// <summary>
		/// Tests whether obj is a CellPadding structure with the same values as 
		/// this Padding structure
		/// </summary>
		/// <param name="obj">The Object to test</param>
		/// <returns>This method returns true if obj is a CellPadding structure 
		/// and its Left, Top, Right, and Bottom properties are equal to 
		/// the corresponding properties of this CellPadding structure; 
		/// otherwise, false</returns>
		public override bool Equals(object obj)
		{
			if (!(obj is CellPadding))
			{
				return false;
			}

			CellPadding padding = (CellPadding) obj;

			if (((padding.Left == this.Left) && (padding.Top == this.Top)) && (padding.Right == this.Right))
			{
				return (padding.Bottom == this.Bottom);
			}

			return false;
		}


		/// <summary>
		/// Returns the hash code for this CellPadding structure
		/// </summary>
		/// <returns>An integer that represents the hashcode for this 
		/// padding</returns>
		public override int GetHashCode()
		{
			return (((this.Left ^ ((this.Top << 13) | (this.Top >> 0x13))) ^ ((this.Right << 0x1a) | (this.Right >> 6))) ^ ((this.Bottom << 7) | (this.Bottom >> 0x19)));
		}

		#endregion


		#region Properties

		/// <summary>
		/// Gets or sets the width of the left padding value
		/// </summary>
		public int Left
		{
			get
			{
				return this.left;
			}

			set
			{
				if (value < 0)
				{
					throw new ArgumentException("Padding value cannot be negative");
				}

				this.left = value;
			}
		}


		/// <summary>
		/// Gets or sets the width of the right padding value
		/// </summary>
		public int Right
		{
			get
			{
				return this.right;
			}

			set
			{
				if (value < 0)
				{
					throw new ArgumentException("Padding value cannot be negative");
				}

				this.right = value;
			}
		}


		/// <summary>
		/// Gets or sets the height of the top padding value
		/// </summary>
		public int Top
		{
			get
			{
				return this.top;
			}

			set
			{
				if (value < 0)
				{
					throw new ArgumentException("Padding value cannot be negative");
				}

				this.top = value;
			}
		}


		/// <summary>
		/// Gets or sets the height of the bottom padding value
		/// </summary>
		public int Bottom
		{
			get
			{
				return this.bottom;
			}

			set
			{
				if (value < 0)
				{
					throw new ArgumentException("Padding value cannot be negative");
				}

				this.bottom = value;
			}
		}


		/// <summary>
		/// Tests whether all numeric properties of this CellPadding have 
		/// values of zero
		/// </summary>
		[Browsable(false)]
		public bool IsEmpty
		{
			get
			{
				if (((this.Left == 0) && (this.Top == 0)) && (this.Right == 0))
				{
					return (this.Bottom == 0);
				}

				return false;
			}
		}

		#endregion


		#region Operators

		/// <summary>
		/// Tests whether two CellPadding structures have equal Left, Top, 
		/// Right, and Bottom properties
		/// </summary>
		/// <param name="left">The CellPadding structure that is to the left 
		/// of the equality operator</param>
		/// <param name="right">The CellPadding structure that is to the right 
		/// of the equality operator</param>
		/// <returns>This operator returns true if the two CellPadding structures 
		/// have equal Left, Top, Right, and Bottom properties</returns>
		public static bool operator ==(CellPadding left, CellPadding right)
		{
			if (((left.Left == right.Left) && (left.Top == right.Top)) && (left.Right == right.Right))
			{
				return (left.Bottom == right.Bottom);
			}

			return false;
		}


		/// <summary>
		/// Tests whether two CellPadding structures differ in their Left, Top, 
		/// Right, and Bottom properties
		/// </summary>
		/// <param name="left">The CellPadding structure that is to the left 
		/// of the equality operator</param>
		/// <param name="right">The CellPadding structure that is to the right 
		/// of the equality operator</param>
		/// <returns>This operator returns true if any of the Left, Top, Right, 
		/// and Bottom properties of the two CellPadding structures are unequal; 
		/// otherwise false</returns>
		public static bool operator !=(CellPadding left, CellPadding right)
		{
			return !(left == right);
		}

		#endregion
	}

	#endregion



	#region CellPaddingConverter

	/// <summary>
	/// A custom TypeConverter used to help convert CellPadding objects from 
	/// one Type to another
	/// </summary>
	internal class CellPaddingConverter : TypeConverter
	{
		/// <summary>
		/// Returns whether this converter can convert an object of the 
		/// given type to the type of this converter, using the specified context
		/// </summary>
		/// <param name="context">An ITypeDescriptorContext that provides 
		/// a format context</param>
		/// <param name="sourceType">A Type that represents the type you 
		/// want to convert from</param>
		/// <returns>true if this converter can perform the conversion; 
		/// otherwise, false</returns>
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			if (sourceType == typeof(string))
			{
				return true;
			}

			return base.CanConvertFrom(context, sourceType);
		}


		/// <summary>
		/// Returns whether this converter can convert the object to the 
		/// specified type, using the specified context
		/// </summary>
		/// <param name="context">An ITypeDescriptorContext that provides a 
		/// format context</param>
		/// <param name="destinationType">A Type that represents the type you 
		/// want to convert to</param>
		/// <returns>true if this converter can perform the conversion; 
		/// otherwise, false</returns>
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			if (destinationType == typeof(InstanceDescriptor))
			{
				return true;
			}
			
			return base.CanConvertTo(context, destinationType);
		}


		/// <summary>
		/// Converts the given object to the type of this converter, using 
		/// the specified context and culture information
		/// </summary>
		/// <param name="context">An ITypeDescriptorContext that provides a 
		/// format context</param>
		/// <param name="culture">The CultureInfo to use as the current culture</param>
		/// <param name="value">The Object to convert</param>
		/// <returns>An Object that represents the converted value</returns>
		public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
		{
			if (value is string)
			{
				string text = ((string) value).Trim();

				if (text.Length == 0)
				{
					return null;
				}

				if (culture == null)
				{
					culture = CultureInfo.CurrentCulture;
				}

				char[] listSeparators = culture.TextInfo.ListSeparator.ToCharArray();

				string[] s = text.Split(listSeparators);

				if (s.Length < 4)
				{
					return null;
				}

				return new CellPadding(int.Parse(s[0]), int.Parse(s[1]), int.Parse(s[2]), int.Parse(s[3]));
			}	
			
			return base.ConvertFrom(context, culture, value);
		}


		/// <summary>
		/// Converts the given value object to the specified type, using 
		/// the specified context and culture information
		/// </summary>
		/// <param name="context">An ITypeDescriptorContext that provides 
		/// a format context</param>
		/// <param name="culture">A CultureInfo object. If a null reference 
		/// is passed, the current culture is assumed</param>
		/// <param name="value">The Object to convert</param>
		/// <param name="destinationType">The Type to convert the value 
		/// parameter to</param>
		/// <returns>An Object that represents the converted value</returns>
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == null)
			{
				throw new ArgumentNullException("destinationType");
			}

			if ((destinationType == typeof(string)) && (value is CellPadding))
			{
				CellPadding p = (CellPadding) value;

				if (culture == null)
				{
					culture = CultureInfo.CurrentCulture;
				}

				string separator = culture.TextInfo.ListSeparator + " ";

				TypeConverter converter = TypeDescriptor.GetConverter(typeof(int));

				string[] s = new string[4];

				s[0] = converter.ConvertToString(context, culture, p.Left);
				s[1] = converter.ConvertToString(context, culture, p.Top);
				s[2] = converter.ConvertToString(context, culture, p.Right);
				s[3] = converter.ConvertToString(context, culture, p.Bottom);

				return string.Join(separator, s);
			}

			if ((destinationType == typeof(InstanceDescriptor)) && (value is CellPadding))
			{
				CellPadding p = (CellPadding) value;

				Type[] t = new Type[4];
				t[0] = t[1] = t[2] = t[3] = typeof(int);

				ConstructorInfo info = typeof(CellPadding).GetConstructor(t);

				if (info != null)
				{
					object[] o = new object[4];

					o[0] = p.Left;
					o[1] = p.Top;
					o[2] = p.Right;
					o[3] = p.Bottom;

					return new InstanceDescriptor(info, o);
				}
			}
			
			return base.ConvertTo(context, culture, value, destinationType);
		}


		/// <summary>
		/// Creates an instance of the Type that this TypeConverter is associated 
		/// with, using the specified context, given a set of property values for 
		/// the object
		/// </summary>
		/// <param name="context">An ITypeDescriptorContext that provides a format 
		/// context</param>
		/// <param name="propertyValues">An IDictionary of new property values</param>
		/// <returns>An Object representing the given IDictionary, or a null 
		/// reference if the object cannot be created</returns>
		public override object CreateInstance(ITypeDescriptorContext context, System.Collections.IDictionary propertyValues)
		{
			return new CellPadding((int) propertyValues["Left"], 
				(int) propertyValues["Top"], 
				(int) propertyValues["Right"], 
				(int) propertyValues["Bottom"]);
		}


		/// <summary>
		/// Returns whether changing a value on this object requires a call to 
		/// CreateInstance to create a new value, using the specified context
		/// </summary>
		/// <param name="context">An ITypeDescriptorContext that provides a 
		/// format context</param>
		/// <returns>true if changing a property on this object requires a call 
		/// to CreateInstance to create a new value; otherwise, false</returns>
		public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
		{
			return true;
		}


		/// <summary>
		/// Returns a collection of properties for the type of array specified 
		/// by the value parameter, using the specified context and attributes
		/// </summary>
		/// <param name="context">An ITypeDescriptorContext that provides a format 
		/// context</param>
		/// <param name="value">An Object that specifies the type of array for 
		/// which to get properties</param>
		/// <param name="attributes">An array of type Attribute that is used as 
		/// a filter</param>
		/// <returns>A PropertyDescriptorCollection with the properties that are 
		/// exposed for this data type, or a null reference if there are no 
		/// properties</returns>
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			PropertyDescriptorCollection collection = TypeDescriptor.GetProperties(typeof(CellPadding), attributes);

			string[] s = new string[4];
			s[0] = "Left";
			s[1] = "Top";
			s[2] = "Right";
			s[3] = "Bottom";

			return collection.Sort(s);
		}


		/// <summary>
		/// Returns whether this object supports properties, using the specified context
		/// </summary>
		/// <param name="context">An ITypeDescriptorContext that provides a format context</param>
		/// <returns>true if GetProperties should be called to find the properties of this 
		/// object; otherwise, false</returns>
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return true;
		}
	}

	#endregion
}
