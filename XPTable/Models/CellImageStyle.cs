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
using System.Drawing;
using System.Windows.Forms;


namespace XPTable.Models
{
	/// <summary>
	/// Stores Image related properties for a Cell
	/// </summary>
	internal class CellImageStyle
	{
		#region Class Data

		/// <summary>
		/// The Image displayed in the Cell
		/// </summary>
		private Image image;

		/// <summary>
		/// Determines how Images are sized in the Cell
		/// </summary>
		private ImageSizeMode imageSizeMode;

		#endregion


		#region Constructor

		/// <summary>
		/// Initializes a new instance of the CellImageStyle class with default settings
		/// </summary>
		public CellImageStyle()
		{
			this.image = null;
			this.imageSizeMode = ImageSizeMode.Normal;
		}

		#endregion


		#region Properties

		/// <summary>
		/// Gets or sets the image that is displayed in the Cell
		/// </summary>
		public Image Image
		{
			get
			{
				return this.image;
			}

			set
			{
				this.image = value;
			}
		}


		/// <summary>
		/// Gets or sets how the Cells image is sized within the Cell
		/// </summary>
		public ImageSizeMode ImageSizeMode
		{
			get
			{
				return this.imageSizeMode;
			}

			set
			{
				if (!Enum.IsDefined(typeof(ImageSizeMode), value)) 
				{
					throw new InvalidEnumArgumentException("value", (int) value, typeof(ImageSizeMode));
				}
				
				if (this.imageSizeMode != value)
				{
					this.imageSizeMode = value;
				}
			}
		}

		#endregion
	}
}
