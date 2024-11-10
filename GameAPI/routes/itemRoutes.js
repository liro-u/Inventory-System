import express from "express";
import itemController from "../controllers/itemController.js";
import requireAdmin from "../middlewares/requireAdmin.js";

const router = express.Router();

// Route to create a new item
router.post("/", requireAdmin, itemController.createItem);
// Route to delete an item
router.delete("/:itemId", requireAdmin, itemController.deleteItem);
// Route to update an item
router.patch("/:itemId", requireAdmin, itemController.updateItem);
// Get Item by ID
router.get("/:itemId", itemController.getItemById);
// Get All Items
router.get("/", itemController.getAllItems);

export default router;
