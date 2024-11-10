import dotenv from "dotenv";
dotenv.config();

import cors from "cors";
import express from "express";
import mongoose from "mongoose";

import morgan from "morgan";

import https from "https";
import fs from "fs";

// routes
import itemRoutes from "./routes/itemRoutes.js";
import userItemRoutes from "./routes/userItemRoutes.js";
import tryAddAuthWithUserAPI from "./middlewares/tryAddAuthUsingUserAPI.js";

// express app
const app = express();

const useHttps = process.env.USE_HTTPS === "true";

// for https certificate
let options = {};
if (useHttps) {
  options = {
    key: fs.readFileSync("localhost-key.pem"),
    cert: fs.readFileSync("localhost.pem"),
  };
}

// middleware
app.use(
  cors({
    origin: "*",
  })
);
app.use(express.json({ limit: "50mb" }));
app.use(express.urlencoded({ limit: "50mb", extended: true }));

app.use(morgan("dev"));

app.use((req, res, next) => {
  tryAddAuthWithUserAPI(req, res, next);
});

// prevent patch method that are obsolete
const patchTimestamp = [];
app.use((req, res, next) => {
  if (req.method === "PATCH") {
    const { timestamp } = req.headers;
    if (patchTimestamp[req.path] && patchTimestamp[req.path] >= timestamp) {
      console.log("request is obsolete");
      return res.status(406).json({ error: "request is obsolete" });
    } else {
      patchTimestamp[req.path] = timestamp;
      console.log("request gonna be treated");
    }
  }
  next();
});

// routes
app.use("/api/items", itemRoutes);
app.use("/api/userItems", userItemRoutes);

// connect to db
console.log("starting server...");
mongoose
  .connect(process.env.MONGO_URI)
  .then(() => {
    console.log("connected to db");
    if (useHttps) {
      https.createServer(options, app).listen(process.env.PORT || 4000, () => {
        console.log("HTTPS Server running on port", process.env.PORT || 4000);
      });
    } else {
      app.listen(process.env.PORT || 4000, () => {
        console.log("listening on port ", process.env.PORT || 4000);
      });
    }
  })
  .catch((err) => {
    console.log(err);
  });
