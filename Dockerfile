FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /publish

COPY ./*.sln ./

# This is a known trick that copies all the csproj files into the image
# and then creates subfolders based on the name of the csproj file.
# The csproj files are copied into the work dir and then moved into their
# respective subfolders
COPY */*.csproj */*/*.csproj ./
RUN dotnet sln list | \
	tail -n +3 | \
	xargs -I {} sh -c \
		'target="{}"; dir="${target%/*}"; file="${target##*/}"; mkdir -p -- "$dir"; mv -- "$file" "$target"'

RUN dotnet restore Asp_net_web_api_template.sln
COPY . .
RUN dotnet publish --output ./dist 

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime

WORKDIR /var/www/rent-cars-api/
COPY --from=build /publish/dist .
ENV ASPNETCORE_URLS "http://0.0.0.0:5000;https://0.0.0.0:5001"
ENV ASPNETCORE_ENVIRONMENT "Development"
ENTRYPOINT ["dotnet", "Api.dll"]
