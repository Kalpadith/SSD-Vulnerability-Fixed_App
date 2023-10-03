// server.js (or any other name you prefer)
const express = require("express");
const httpProxy = require("http-proxy");
const { createProxyMiddleware } = require("http-proxy-middleware");
const helmet = require("helmet");

const app = express();

app.use(helmet());
app.disable("x-powered-by");

//Set up a proxy to React development server
const proxy = httpProxy.createProxyServer();
const reactServerUrl = "http://localhost:3000";

app.use(
  "/app",
  createProxyMiddleware({ target: reactServerUrl, changeOrigin: true })
);

const port = process.env.PORT || 4000;
app.listen(port, () => {
  console.log(`Proxy server is running on port ${port}`);
});
