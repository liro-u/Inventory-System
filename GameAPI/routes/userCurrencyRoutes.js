import express from "express";
import requireAuth from "../middlewares/requireAuth.js";
import userCurrencyController from "../controllers/userCurrencyController.js";

const router = express.Router();

// Add Currency Quantity To User Inventory
router.patch(
  "/addCurrencyQuantity",
  requireAuth,
  userCurrencyController.addCurrencyToUser
);
// Remove Currency Quantity From User Inventory
router.patch(
  "/removeCurrencyQuantity",
  requireAuth,
  userCurrencyController.removeCurrencyFromUser
);
// Get All Currency for User Inventory
router.get("/", requireAuth, userCurrencyController.getUserCurrencies);
// Get Specific Currency Details for User Inventory
router.get("/:currencyId", requireAuth, userCurrencyController.getUserCurrency);

export default router;
