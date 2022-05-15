# Fsharp4Me

# First steps

## Install dotnet CLI (TODO)
## NeoVim configs
Install LSP
```bash
dotnet tool install -g fsautocomplete
```
Plugs:
```vim script
Plug 'autozimu/LanguageClient-neovim', {'branch': 'next', 'do': 'bash install.sh'}
Plug 'PhilT/vim-fsharp'
Plug 'fsharp/FsAutoComplete'
```
Config for language client:
```vim script
let g:LanguageClient_serverCommands = {
   \   'ocaml':           ['ocamllsp'],
   \   'go':              ['gopls'],
   \   'fsharp':          ['fsautocomplete'], 
\}
```
## Start a new F# project
```bash
dotnet new console -lang "F#" -o src/App
```
For more examples or options try these two options:
```bash
dotnet new --help
```
```bash
dotnet new
```
## Run the project
At the folder with `App.fsproj`:
```bash
dotnet run <args>
```

To run with watch:
```bash
dotnet watch run <args>
```

To run a single script:
```bash
fsharpi <file_name>.fsx <args>
```
or
```bash
dotnet fsi <file_name>.fsx <args>
```
<img src="https://github.com/OnofreTZK/Fsharp4Me/blob/main/introduction/HelloWorldFsharp/src/App/images/Captura%20de%20tela%20de%202022-04-30%2022-02-22.png">

# [Fsharp awesome projects](https://github.com/fsprojects/awesome-fsharp)
# [Dusted codes about dotnet and fsharp](https://dusted.codes/)
