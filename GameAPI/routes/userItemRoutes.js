import express from "express";
import userItemController from "../controllers/userItemController.js";
import requireAuth from "../middlewares/requireAuth.js";

const router = express.Router();

// Add Item Quantity To User Inventory
router.patch("/addItemQuantity", requireAuth, userItemController.addItemToUser);
// Remove Item Quantity From User Inventory
router.patch(
  "/removeItemQuantity",
  requireAuth,
  userItemController.removeItemFromUser
);
// Get All Items for User Inventory
router.get("/", requireAuth, userItemController.getUserItems);
// Get Specific Item Details for User Inventory
router.get("/:itemId", requireAuth, userItemController.getUserItem);

export default router;
