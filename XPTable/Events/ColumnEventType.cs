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


namespace XPTable.Events
{
    /// <summary>
    /// Specifies the type of event generated when the value of a 
    /// Column's property changes
    /// </summary>
    public enum ColumnEventType
    {
        /// <summary>
        /// Occurs when the Column's property change type is unknown
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Occurs when the value of a Column's Text property changes
        /// </summary>
        TextChanged = 1,

        /// <summary>
        /// Occurs when the value of a Column's Alignment property changes
        /// </summary>
        AlignmentChanged = 2,

        /// <summary>
        /// Occurs when the value of a Column's HeaderAlignment property changes
        /// </summary>
        HeaderAlignmentChanged = 3,

        /// <summary>
        /// Occurs when the value of a Column's Width property changes
        /// </summary>
        WidthChanged = 4,

        /// <summary>
        /// Occurs when the value of a Column's Visible property changes
        /// </summary>
        VisibleChanged = 5,

        /// <summary>
        /// Occurs when the value of a Column's Image property changes
        /// </summary>
        ImageChanged = 6,

        /// <summary>
        /// Occurs when the value of a Column's Format property changes
        /// </summary>
        FormatChanged = 7,

        /// <summary>
        /// Occurs when the value of a Column's ColumnState property changes
        /// </summary>
        StateChanged = 8,

        /// <summary>
        /// Occurs when the value of a Column's Renderer property changes
        /// </summary>
        RendererChanged = 9,

        /// <summary>
        /// Occurs when the value of a Column's Editor property changes
        /// </summary>
        EditorChanged = 10,

        /// <summary>
        /// Occurs when the value of a Column's Comparer property changes
        /// </summary>
        ComparerChanged = 11,

        /// <summary>
        /// Occurs when the value of a Column's Enabled property changes
        /// </summary>
        EnabledChanged = 12,

        /// <summary>
        /// Occurs when the value of a Column's Editable property changes
        /// </summary>
        EditableChanged = 13,

        /// <summary>
        /// Occurs when the value of a Column's Selectable property changes
        /// </summary>
        SelectableChanged = 14,

        /// <summary>
        /// Occurs when the value of a Column's Sortable property changes
        /// </summary>
        SortableChanged = 15,

        /// <summary>
        /// Occurs when the value of a Column's SortOrder property changes
        /// </summary>
        SortOrderChanged = 16,

        /// <summary>
        /// Occurs when the value of a Column's ToolTipText property changes
        /// </summary>
        ToolTipTextChanged = 17,

        /// <summary>
        /// Occurs when a Column is being sorted
        /// </summary>
        Sorting = 18,

        // Mateusz [PEYN] Adamus (peyn@tlen.pl)
        /// <summary>
        /// Occurs when the value of a Column's Resizable property changes
        /// </summary>
        ResizableChanged = 19,

        /// <summary>
        /// Occurs when the value of a Column's AutoResizeMode property changes
        /// </summary>
        AutoResizeModeChanged = 20
    }
}
