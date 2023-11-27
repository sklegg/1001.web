del wwwroot\appsettings.json

copy ..\env_configs\appsettings-prod.json wwwroot\appsettings.json

rmdir /s /q bin

rmdir /s /q obj

dotnet publish -c Release

aws s3 rm s3://1001-web/ --recursive

aws s3 cp bin/Release/net7.0/publish/wwwroot s3://1001-web/ --recursive

aws cloudfront create-invalidation --distribution-id E1667IMT8ZU1TR --paths "/*"
