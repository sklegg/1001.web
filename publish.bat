del wwwroot\appsettings.json

copy ..\env_configs\appsettings-dev.json wwwroot\appsettings.json
 
rmdir /s /q bin

rmdir /s /q obj

dotnet publish -c Release

aws s3 rm s3://1001-web-dev/ --recursive

aws s3 cp bin/Release/net7.0/publish/wwwroot s3://1001-web-dev/ --recursive

aws cloudfront create-invalidation --distribution-id E2QFE7HP6WYPA3 --paths "/*"
