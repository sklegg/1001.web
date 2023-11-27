To release:
run dotnet publish -c Release
copy bin/Release/net7.0/publish/wwwroot to the S3 bucket
create a new Cloudfront invalidation