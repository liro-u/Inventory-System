import Item from "../models/itemModel.js";

// Create Item
const createItem = async (req, res) => {
  try {
    const { name, description, type, rarity, maxQuantityPerSlot, maxSlot } =
      req.body;

    // Validate required fields
    if (!name || !description) {
      return res.status(400).json({
        errors: {
          global: "Name and description are required",
        },
      });
    }

    // Create a new item
    const newItem = new Item({
      name,
      description,
      type,
      rarity,
      maxQuantityPerSlot,
      maxSlot,
    });

    // Save the item to the database
    await newItem.save();

    // Send success response
    res.status(201).json({
      message: "Item created successfully",
      item: newItem,
    });
  } catch (error) {
    console.error(error);
    res.status(500).json({ errors: { global: "Failed to create item" } });
  }
};

// Delete Item
const deleteItem = async (req, res) => {
  try {
    const { itemId } = req.params;

    // Find the item by ID and delete it
    const deletedItem = await Item.findByIdAndDelete(itemId);

    if (!deletedItem) {
      return res.status(404).json({ errors: { global: "Item not found" } });
    }

    // Send success response
    res.status(200).json({
      message: "Item deleted successfully",
      item: deletedItem,
    });
  } catch (error) {
    console.error(error);
    res.status(500).json({ errors: { global: "Failed to delete item" } });
  }
};

// Update Item
const updateItem = async (req, res) => {
  try {
    const { itemId } = req.params;
    const updateFields = req.body; // Take only fields that are provided

    // Find the item by ID and update with partial data
    const updatedItem = await Item.findByIdAndUpdate(itemId, updateFields, {
      new: true,
    });

    if (!updatedItem) {
      return res.status(404).json({ errors: { global: "Item not found" } });
    }

    // Send success response
    res.status(200).json({
      message: "Item updated successfully",
      item: updatedItem,
    });
  } catch (error) {
    console.error(error);
    res.status(500).json({ errors: { global: "Failed to update item" } });
  }
};

// Get Item by ID
export const getItemById = async (req, res) => {
  try {
    const { itemId } = req.params;
    const item = await Item.findById(itemId);
    if (!item) {
      return res.status(404).json({ errors: { global: "Item not found" } });
    }
    res.status(200).json(item);
  } catch (error) {
    res.status(500).json({ errors: { global: "Failed to fetch item" } });
  }
};

// Get All Items
export const getAllItems = async (req, res) => {
  try {
    const items = await Item.find();
    res.status(200).json(items);
  } catch (error) {
    res.status(500).json({ errors: { global: "Failed to fetch items" } });
  }
};

export default {
  createItem,
  deleteItem,
  updateItem,
  getItemById,
  getAllItems,
};
