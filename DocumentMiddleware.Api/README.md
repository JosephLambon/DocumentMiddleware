1. Install Docker


2. Run docker container image
```
docker start postgres
```

```
# IF NOT INSTALLED, RUN THIS:
docker pull postgres
docker run --name postgres-db -e POSTGRES_PASSWORD=testpassword -e POSTGRES_USER=testuser -e POSTGRES_DB=mydatabase -p 5432:5432 -v postgres-data:/var/lib/postgresql/data -d postgres
docker ps # confirm it's running
```


2. Update appsettings.json with your PostgreSQL connection string

```
"ConnectionStrings": {
	"default": "Server=localhost,5432;Database=mydatabase;User Id=testuser;Password=testpassword;"
}
```

3. Run the application