import express from "express";
import currencyController from "../controllers/currencyController.js";
import requireAdmin from "../middlewares/requireAdmin.js";

const router = express.Router();

// Route to create a new item
router.post("/", requireAdmin, currencyController.createCurrency);
// Route to delete an item
router.delete("/:currencyId", requireAdmin, currencyController.deleteCurrency);
// Route to update an item
router.patch("/:currencyId", requireAdmin, currencyController.updateCurrency);
// Get Item by ID
router.get("/:currencyId", currencyController.getCurrencyById);
// Get All Items
router.get("/", currencyController.getAllCurrencies);

export default router;
