
# Компоненты
## ExpandingTextBlock
Расширяется и сворачивается с анимацией
```XAML
<components:ExpandingTextBlock
           Text="{Binding MapObject.Description}"
           UseAnimation="{Binding IsLoaded}"
           Foreground="{StaticResource PrimaryForegroundBrush}"
           Style="{StaticResource DescriptionTextStyle}"
           CollapsedHeight="140"
           IsExpanded="{Binding IsDetailsOpen}"
           AnimationDuration="0:0:0.25"
           FontSize="15"/>
```
## PlaceholderTextBox
Поле ввода текста с плейсхолдером
```XAML
<components:PlaceholderTextBox
    x:Name="Search"
    Text="{Binding SearchText, Mode=OneWayToSource,UpdateSourceTrigger=PropertyChanged}"
    Foreground="{StaticResource MainFillBrush}"
    BorderBrush="{StaticResource MainFillBrush}"
    FontSize="20"
    Placeholder="Поиск..."
    PlaceholderBrush="{StaticResource PlaceholderForegroundBrush}"
    FontFamily="{StaticResource SegoeFontFamily}"
    VerticalContentAlignment="Center"
    Padding="40 0"
    CornerRadius="32"/>
```

## HeaderedPlaceholderTextBox
Поле ввода текста с заголовком и плейсхолдером
```XAML
<components:HeaderedPlaceholderTextBox
    Text="{Binding Name, Mode=OneWayToSource,UpdateSourceTrigger=PropertyChanged}"
    Foreground="{StaticResource MainFillBrush}"
    BorderBrush="{StaticResource MainFillBrush}"
    FontSize="20"
    Header="Имя"
    HeaderStyle="{StaticResource MainHeaderStyle}"
    Placeholder="Введите имя"
    PlaceholderBrush="{StaticResource PlaceholderForegroundBrush}"
    FontFamily="{StaticResource SegoeFontFamily}"
    VerticalContentAlignment="Center"
    Padding="40 0"
    CornerRadius="32"/>
```
## IntCounter
Поле ввода числа с заданным диапозоном и двумя кнопками (+,-)
```XAML
<components:IntCounter
            Value="{Binding CountAdult}"
            Max="{Binding CurrentServiceHolding.MaxTicketsAdult}"
            Min=0
            HorizontalAlignment="Right"/>
```
## ModalControl
Бэкдроп для попапов
```XAML
 <components:ModalControl 
     Panel.ZIndex="1"
     IsOpen="{Binding IsModalOpen}">
     <ContentControl Content="{Binding CurrentModalViewModel}"/>
 </components:ModalControl>
```
## MultiSwitchButton
Анимированная кнопка-переключатель с множеством вариантов
```XAML
 <components:MultiSwitchButton
     ItemsSource="{Binding Pages}"
     Width="704"
     Margin="20 0"
     CornerRadius="25"
     SelectedItem="{Binding CurrentPage,Mode=TwoWay}"/>
```
## RepeatingMediaElement
MediaElement с автоматически повторяющимся контентом
```XAML
<components:RepeatingMediaElement
      Source="{Binding MediaSource}"/>
```
## RoundedButton и RoundedUserControl
Кнопка и UserControl с поддержкой скругления углов
```XAML
<components:RoundedButton
      CornerRadius="28"/>
```
## RoundedLimitedHeightImage
Скругленная картинка ограниченная по высоте и неограниченная по ширине (картинка не обрезается)
```XAML
<components:RoundedLimitedHeightImage
    Margin="0 32"
    Height="476"
    CornerRadius="32"
    ImageSource="{Binding Item.Entity.ImagePath}"/>
```

# Панели
## StretchyWrapPanel
Панель, равномерно растягивающая элементы для заполнения свободного пространства
```XAML
<panels:StretchyWrapPanel
    Orientation="Horizontal"
    StretchProportionally="True"
    ItemHeight="{Binding ActualHeight,RelativeSource={RelativeSource FindAncestor,AncestorType=Grid}}"/>
```
## OverlappingWrapPanel
Панель, наслаивающая элементы друг на друга при нехватке места. InitialMargin - стартовый отступ между элементами
```XAML
<panels:OverlappingWrapPanel
    Orientation="Horizontal"
    ItemInitialMargin=10/>
```
## MiddleAnchoringCanvas
Канвас, выставляющий элементы по центру, а не по верхнему левому углу от Canvas.Left и Canvas.Top

# Контейнеры для попапов
## ScalePopupContainer
Контейнер с анимацией увеличения размера



## OpacityPopupContainer
Контейнер с fade анимацией  
