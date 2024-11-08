# Диалоговое окно Avalonia средствами PowerShell

Версия PowerShell не ниже 6.

Перед использованием необходимо установить модуль `PSAvalonia`:

```
Install-Module -Name PSAvalonia
```

Чтобы не заморачиваться с правами администратора, можно установить для текущего пользователя:

```
Install-Module -Name PSAvalonia -Scope CurrentUser
```

Скрипт запускается так:

```
powershell -File app.py
```
