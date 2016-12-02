docker build -t build-image --build-arg TAG="lambda" -f Dockerfile.build .
docker create --name build-cont build-image
docker cp build-cont:/app/publish/. publish/
cd publish; zip -r ../publish.zip *
rm -rf publish/
docker rm build-cont