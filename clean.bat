dir /B /S "_ReSharper*" /AD >r.list & for /F "delims=" %%a in (r.list) DO (attrib -H "%%a" & rd /Q /S "%%a")

dir /B /S "Debug" /AD >d.list & for /F "delims=" %%a in (d.list) DO (attrib -H "%%a" & rd /Q /S "%%a")
dir /B /S "Release" /AD >e.list & for /F "delims=" %%a in (e.list) DO (attrib -H "%%a" & rd /Q /S "%%a")
dir /B /S "bin" /AD >a.list & for /F "delims=" %%a in (a.list) DO (attrib -H "%%a" & rd /Q /S "%%a")
dir /B /S "obj" /AD >b.list & for /F "delims=" %%a in (b.list) DO (attrib -H "%%a" & rd /Q /S "%%a")

rem dir /B /S "x64" /AD >f.list & for /F "delims=" %%a in (f.list) DO (attrib -H "%%a" & rd /Q /S "%%a")
dir /B /S "AppPackages" /AD >g.list & for /F "delims=" %%a in (g.list) DO (attrib -H "%%a" & rd /Q /S "%%a")
dir /B /S "ipch" /AD >h.list & for /F "delims=" %%a in (h.list) DO (attrib -H "%%a" & rd /Q /S "%%a")

rem attrib -H /S *.suo
rem del /Q /S *.suo
attrib -H /S *.*sdf
del /Q /S *.*sdf

rem GOTO EOF

del a.list
del b.list
del c.list
del d.list
del e.list
rem del f.list
del g.list
del h.list
del r.list

:EOF