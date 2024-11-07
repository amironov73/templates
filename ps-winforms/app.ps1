Add-Type -AssemblyName System.Windows.Forms
Add-Type -AssemblyName System.Drawing

$form = New-Object System.Windows.Forms.Form
$form.Text = "Hello World Form"
$form.Size = New-Object System.Drawing.Size(200, 300)

$panel = New-Object System.Windows.Forms.TableLayoutPanel
$panel.Padding = New-Object System.Windows.Forms.Padding(10)
$panel.Dock = [System.Windows.Forms.DockStyle]::Fill

$first = New-Object System.Windows.Forms.TextBox
$first.Margin = New-Object System.Windows.Forms.Padding(5)
$first.Dock = [System.Windows.Forms.DockStyle]::Fill
$first.Text = "123"
$panel.Controls.Add($first)

$second = New-Object System.Windows.Forms.TextBox
$second.Margin = New-Object System.Windows.Forms.Padding(5)
$second.Dock = [System.Windows.Forms.DockStyle]::Fill
$second.Text = "456"
$panel.Controls.Add($second)

$button = New-Object System.Windows.Forms.Button
$button.Margin = New-Object System.Windows.Forms.Padding(5)
$button.Dock = [System.Windows.Forms.DockStyle]::Fill
$button.Text = "Сложить числа"
$panel.Controls.Add($button)

$sum = New-Object System.Windows.Forms.TextBox
$sum.Margin = New-Object System.Windows.Forms.Padding(5)
$sum.Dock = [System.Windows.Forms.DockStyle]::Fill
$sum.ReadOnly = [bool]'True'
$panel.Controls.Add($sum);

$button.Add_Click({
    $one = [System.Int32]::Parse($first.Text)
    $two = [System.Int32]::Parse($second.Text)
    $three = $one + $two
    $sum.Text = [System.Convert]::ToString($three)
})

$form.Controls.Add($panel)
$form.ShowDialog() | Out-Null
