���� ������ couchdb ��� ����������

���������� ������:

msbuild /t:pack /p:Configuration=Release

nuget push Cmas.Backend.Modules.Contracts.Datalayer.Couchdb.1.0.0.nupkg -Source http://cm-ylng-msk-04/nuget/nuget