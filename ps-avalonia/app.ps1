$Xaml = '<Window xmlns="https://github.com/avaloniaui"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        mc:Ignorable="d"
        x:Class="ps_avalonia.MainWindow"
        Width="300"
        Height="200"
        Title="Авалония плюс ПоверШелл">
    <StackPanel HorizontalAlignment="Stretch" VerticalAlignment="Stretch"
        Orientation="Vertical" Margin="10">
        <TextBox x:Name="First" Text="123" Margin="5" />
        <TextBox x:Name="Second" Text="456" Margin="5" />
        <Button x:Name="Button" Margin="5" HorizontalAlignment="Center">
            Нажми меня
        </Button>
        <TextBox x:Name="Third" Margin="5"/>

    </StackPanel>
</Window>'
$window = ConvertTo-AvaloniaWindow -Xaml $Xaml
$button = Find-AvaloniaControl -Name 'Button' -Window $Window
$first = Find-AvaloniaControl -Name 'First' -Window $Window
$second = Find-AvaloniaControl -Name 'Second' -Window $Window
$third = Find-AvaloniaControl -Name 'Third' -Window $Window
$Button.add_Click({
    $inc = [System.Globalization.CultureInfo]::InvariantCulture
    $one = [System.Double]::Parse($first.Text, $inc)
    $two = [System.Double]::Parse($second.Text, $inc)
    $three = $one + $two
    $third.Text = $three.ToString($inc)
})
Show-AvaloniaWindow -Window $Window