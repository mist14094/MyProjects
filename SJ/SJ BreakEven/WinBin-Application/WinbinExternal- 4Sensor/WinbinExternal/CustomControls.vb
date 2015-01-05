Imports System.Windows.Markup
Imports System.Globalization

    Public Class OutlinedText
        Inherits FrameworkElement
        Implements IAddChild

        Public Sub AddChild(value As Object) Implements System.Windows.Markup.IAddChild.AddChild

        End Sub

        Public Sub AddText(text As String) Implements System.Windows.Markup.IAddChild.AddText

        End Sub

#Region "Private Fields"

        Private _textGeometry As Geometry

#End Region

#Region "Private Methods"

        ''' <summary>
        ''' Invoked when a dependency property has changed. Generate a new FormattedText object to display.
        ''' </summary>
        ''' <param name="d">OutlineText object whose property was updated.</param>
        ''' <param name="e">Event arguments for the dependency property.</param>
        Private Shared Sub OnOutlineTextInvalidated(d As DependencyObject, e As DependencyPropertyChangedEventArgs)
            DirectCast(d, OutlinedText).CreateText()
        End Sub

#End Region


#Region "FrameworkElement Overrides"

        ''' <summary>
        ''' OnRender override draws the geometry of the text and optional highlight.
        ''' </summary>
        ''' <param name="drawingContext">Drawing context of the OutlineText control.</param>
        Protected Overrides Sub OnRender(drawingContext As DrawingContext)
            CreateText()
            ' Draw the outline based on the properties that are set.
            drawingContext.DrawGeometry(Fill, New Pen(Stroke, StrokeThickness), _textGeometry)

        End Sub

        ''' <summary>
        ''' Create the outline geometry based on the formatted text.
        ''' </summary>
        Public Sub CreateText()
            Dim fontStyle As FontStyle = FontStyles.Normal
            Dim fontWeight As FontWeight = FontWeights.Medium

            If Bold = True Then
                fontWeight = FontWeights.Bold
            End If
            If Italic = True Then
                fontStyle = FontStyles.Italic
            End If

            ' Create the formatted text based on the properties set.
            ' This brush does not matter since we use the geometry of the text. 
            Dim formattedText As New FormattedText(Text, CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight, New Typeface(Font, fontStyle, fontWeight, FontStretches.Normal), FontSize, Brushes.Black)

            ' Build the geometry object that represents the text.
            _textGeometry = formattedText.BuildGeometry(New Point(0, 0))




            'set the size of the custome control based on the size of the text
            Me.MinWidth = formattedText.Width
            Me.MinHeight = formattedText.Height

        End Sub

#End Region

#Region "DependencyProperties"

        ''' <summary>
        ''' Specifies whether the font should display Bold font weight.
        ''' </summary>
        Public Property Bold() As Boolean
            Get
                Return CBool(GetValue(BoldProperty))
            End Get

            Set(value As Boolean)
                SetValue(BoldProperty, value)
            End Set
        End Property

        ''' <summary>
        ''' Identifies the Bold dependency property.
        ''' </summary>
        Public Shared ReadOnly BoldProperty As DependencyProperty = DependencyProperty.Register("Bold", GetType(Boolean), GetType(OutlinedText), New FrameworkPropertyMetadata(False, FrameworkPropertyMetadataOptions.AffectsRender, New PropertyChangedCallback(AddressOf OnOutlineTextInvalidated), Nothing))

        ''' <summary>
        ''' Specifies the brush to use for the fill of the formatted text.
        ''' </summary>
        Public Property Fill() As Brush
            Get
                Return DirectCast(GetValue(FillProperty), Brush)
            End Get

            Set(value As Brush)
                SetValue(FillProperty, value)
            End Set
        End Property

        ''' <summary>
        ''' Identifies the Fill dependency property.
        ''' </summary>
        Public Shared ReadOnly FillProperty As DependencyProperty = DependencyProperty.Register("Fill", GetType(Brush), GetType(OutlinedText), New FrameworkPropertyMetadata(New SolidColorBrush(Colors.LightSteelBlue), FrameworkPropertyMetadataOptions.AffectsRender, New PropertyChangedCallback(AddressOf OnOutlineTextInvalidated), Nothing))

        ''' <summary>
        ''' The font to use for the displayed formatted text.
        ''' </summary>
        Public Property Font() As FontFamily
            Get
                Return DirectCast(GetValue(FontProperty), FontFamily)
            End Get

            Set(value As FontFamily)
                SetValue(FontProperty, value)
            End Set
        End Property

        ''' <summary>
        ''' Identifies the Font dependency property.
        ''' </summary>
        Public Shared ReadOnly FontProperty As DependencyProperty = DependencyProperty.Register("Font", GetType(FontFamily), GetType(OutlinedText), New FrameworkPropertyMetadata(New FontFamily("Arial"), FrameworkPropertyMetadataOptions.AffectsRender, New PropertyChangedCallback(AddressOf OnOutlineTextInvalidated), Nothing))

        ''' <summary>
        ''' The current font size.
        ''' </summary>
        Public Property FontSize() As Double
            Get
                Return CDbl(GetValue(FontSizeProperty))
            End Get

            Set(value As Double)
                SetValue(FontSizeProperty, value)
            End Set
        End Property

        ''' <summary>
        ''' Identifies the FontSize dependency property.
        ''' </summary>
        Public Shared ReadOnly FontSizeProperty As DependencyProperty = DependencyProperty.Register("FontSize", GetType(Double), GetType(OutlinedText), New FrameworkPropertyMetadata(CDbl(48.0), FrameworkPropertyMetadataOptions.AffectsRender, New PropertyChangedCallback(AddressOf OnOutlineTextInvalidated), Nothing))


        ''' <summary>
        ''' Specifies whether the font should display Italic font style.
        ''' </summary>
        Public Property Italic() As Boolean
            Get
                Return CBool(GetValue(ItalicProperty))
            End Get

            Set(value As Boolean)
                SetValue(ItalicProperty, value)
            End Set
        End Property

        ''' <summary>
        ''' Identifies the Italic dependency property.
        ''' </summary>
        Public Shared ReadOnly ItalicProperty As DependencyProperty = DependencyProperty.Register("Italic", GetType(Boolean), GetType(OutlinedText), New FrameworkPropertyMetadata(False, FrameworkPropertyMetadataOptions.AffectsRender, New PropertyChangedCallback(AddressOf OnOutlineTextInvalidated), Nothing))

        ''' <summary>
        ''' Specifies the brush to use for the stroke and optional hightlight of the formatted text.
        ''' </summary>
        Public Property Stroke() As Brush
            Get
                Return DirectCast(GetValue(StrokeProperty), Brush)
            End Get

            Set(value As Brush)
                SetValue(StrokeProperty, value)
            End Set
        End Property

        ''' <summary>
        ''' Identifies the Stroke dependency property.
        ''' </summary>
        Public Shared ReadOnly StrokeProperty As DependencyProperty = DependencyProperty.Register("Stroke", GetType(Brush), GetType(OutlinedText), New FrameworkPropertyMetadata(New SolidColorBrush(Colors.Teal), FrameworkPropertyMetadataOptions.AffectsRender, New PropertyChangedCallback(AddressOf OnOutlineTextInvalidated), Nothing))

        ''' <summary>
        '''     The stroke thickness of the font.
        ''' </summary>
        Public Property StrokeThickness() As UShort
            Get
                Return CUShort(GetValue(StrokeThicknessProperty))
            End Get

            Set(value As UShort)
                SetValue(StrokeThicknessProperty, value)
            End Set
        End Property

        ''' <summary>
        ''' Identifies the StrokeThickness dependency property.
        ''' </summary>
        Public Shared ReadOnly StrokeThicknessProperty As DependencyProperty = DependencyProperty.Register("StrokeThickness", GetType(UShort), GetType(OutlinedText), New FrameworkPropertyMetadata(CUShort(0), FrameworkPropertyMetadataOptions.AffectsRender, New PropertyChangedCallback(AddressOf OnOutlineTextInvalidated), Nothing))

        ''' <summary>
        ''' Specifies the text string to display.
        ''' </summary>
        Public Property Text() As String
            Get
                Return DirectCast(GetValue(TextProperty), String)
            End Get

            Set(value As String)
                SetValue(TextProperty, value)
            End Set
        End Property

        ''' <summary>
        ''' Identifies the Text dependency property.
        ''' </summary>
        Public Shared ReadOnly TextProperty As DependencyProperty = DependencyProperty.Register("Text", GetType(String), GetType(OutlinedText), New FrameworkPropertyMetadata("", FrameworkPropertyMetadataOptions.AffectsRender, New PropertyChangedCallback(AddressOf OnOutlineTextInvalidated), Nothing))

#End Region
    End Class