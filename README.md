# codestat
Utility that calculates simple code statistics
## usage:
codestat <comand> [options]
commands:
kloc  - calculates lines of code

options:
--source    [-s]  -specify source directory
--include   [-i]  -allows to specify file/directory mask seprated by coma to include in stat\
                  examle -i *.cs,*.js
--exclude   [-e]  -allows to specify file/directory mask separated by coma to exclude from stat
                  example -e bin/, test/ (bin/, obj/) excluded by default
