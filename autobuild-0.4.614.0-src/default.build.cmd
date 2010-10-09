@echo off

lib\nant\bin\nant -f:default.build -D:configuration=release
pause