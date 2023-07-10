#stage 1
FROM node:latest as node
WORKDIR /app
COPY . .
RUN npm install
RUN npm run build
#stage 2
FROM nginx:alpine
EXPOSE 4200
COPY --from=node /app/dist/scenius-codetest /usr/share/nginx/html