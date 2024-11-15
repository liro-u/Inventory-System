import Currency from "../models/currencyModel.js";

// Create Currency
const createCurrency = async (req, res) => {
  try {
    const { name, description, maxQuantity } = req.body;

    // Validate required fields
    if (!name || !description) {
      return res.status(400).json({
        errors: {
          global: "Name and description are required",
        },
      });
    }

    // Create a new currency
    const newCurrency = new Currency({
      name,
      description,
      maxQuantity,
    });

    // Save the currency to the database
    await newCurrency.save();

    // Send success response
    res.status(201).json({
      message: "Currency created successfully",
      currency: newCurrency,
    });
  } catch (error) {
    console.error(error);
    res.status(500).json({ errors: { global: "Failed to create currency" } });
  }
};

// Delete Currency
const deleteCurrency = async (req, res) => {
  try {
    const { currencyId } = req.params;

    // Find the currency by ID and delete it
    const deletedCurrency = await Currency.findByIdAndDelete(currencyId);

    if (!deletedCurrency) {
      return res.status(404).json({ errors: { global: "Currency not found" } });
    }

    // Send success response
    res.status(200).json({
      message: "Currency deleted successfully",
      currency: deleteCurrency,
    });
  } catch (error) {
    console.error(error);
    res.status(500).json({ errors: { global: "Failed to delete currency" } });
  }
};

// Update Currency
const updateCurrency = async (req, res) => {
  try {
    const { currencyId } = req.params;
    const updateFields = req.body; // Take only fields that are provided

    // Find the currency by ID and update with partial data
    const updatedCurrency = await Currency.findByIdAndUpdate(
      currencyId,
      updateFields,
      {
        new: true,
      }
    );

    if (!updateCurrency) {
      return res.status(404).json({ errors: { global: "Currency not found" } });
    }

    // Send success response
    res.status(200).json({
      message: "Currency updated successfully",
      currency: updatedCurrency,
    });
  } catch (error) {
    console.error(error);
    res.status(500).json({ errors: { global: "Failed to update currency" } });
  }
};

// Get Currency by ID
export const getCurrencyById = async (req, res) => {
  try {
    const { currencyId } = req.params;
    const currency = await Currency.findById(currencyId);
    if (!currency) {
      return res.status(404).json({ errors: { global: "Currency not found" } });
    }
    res.status(200).json(currency);
  } catch (error) {
    res.status(500).json({ errors: { global: "Failed to fetch currency" } });
  }
};

// Get All Currencies
export const getAllCurrencies = async (req, res) => {
  try {
    const currencies = await Currency.find();
    res.status(200).json({ currencies });
  } catch (error) {
    res.status(500).json({ errors: { global: "Failed to fetch currency" } });
  }
};

export default {
  createCurrency,
  deleteCurrency,
  updateCurrency,
  getCurrencyById,
  getAllCurrencies,
};
