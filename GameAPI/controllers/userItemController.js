import UserItem from "../models/userItemModel.js";
import Item from "../models/itemModel.js";

/**
 * Controller to add item quantity to user inventory
 * @param req The request data
 * @param res The result of the request
 * @returns {Promise<*>}
 */
export const addItemToUser = async (req, res) => {
  const { itemId, quantity } = req.body;
  const userId = req.user._id;

  if (!itemId || quantity <= 0) {
    return res
      .status(400)
      .json({ errors: { global: "Invalid item ID or quantity." } });
  }

  try {
    // Retrieve the maximum quantity per slot and the maximum number of slots the item can take.
    const itemData = await Item.findOne({ _id: itemId }, [
      "maxQuantityPerSlot",
      "maxSlot",
    ]).exec();

    // Retrieve the existing number of items.
    const userItem = await UserItem.findOne({ userId, itemId }, [
      "quantity",
    ]).exec();
    let existingQuantity = 0;
    if (userItem) {
      existingQuantity = userItem.quantity;
    }

    // Calculate the number of items that will not be added if the max number is reached.
    let remainingQuantity = 0;
    let addingQuantity = quantity;
    if (
      itemData.maxSlot > -1 &&
      itemData.maxQuantityPerSlot > -1 &&
      existingQuantity + quantity >
        itemData.maxSlot * itemData.maxQuantityPerSlot
    ) {
      remainingQuantity =
        existingQuantity +
        quantity -
        itemData.maxSlot * itemData.maxQuantityPerSlot;
      addingQuantity -= remainingQuantity;
    }

    // Find or create the UserItem.
    const userItemUpdated = await UserItem.findOneAndUpdate(
      { userId, itemId },
      { $inc: { quantity: addingQuantity } },
      { new: true, upsert: true, setDefaultsOnInsert: true }
    );

    res.status(200).json({
      userItem: userItemUpdated,
      remainingQuantity: remainingQuantity,
    });
  } catch (error) {
    res
      .status(500)
      .json({ errors: { global: "An error occurred. - " + error.message } });
  }
};

// Controller to remove item quantity from user inventory
export const removeItemFromUser = async (req, res) => {
  const { itemId, quantity } = req.body;
  const userId = req.user._id;

  if (!itemId || quantity <= 0) {
    return res
      .status(400)
      .json({ errors: { global: "Invalid item ID or quantity." } });
  }

  try {
    // Find the UserItem
    const userItem = await UserItem.findOne({ userId, itemId });

    if (!userItem) {
      return res
        .status(404)
        .json({ errors: { global: "Item not found for the user." } });
    }

    if (userItem.quantity < quantity) {
      return res
        .status(400)
        .json({ errors: { global: "Insufficient quantity." } });
    }

    userItem.quantity -= quantity;

    if (userItem.quantity === 0) {
      // Delete the item if quantity reaches 0
      await userItem.deleteOne();
      return res
        .status(200)
        .json({ errors: { global: "Item removed completely." } });
    } else {
      await userItem.save();
      res.status(200).json(userItem);
    }
  } catch (error) {
    res
      .status(500)
      .json({ errors: { global: "An error occurred. - " + error.message } });
  }
};

// get user Items
export const getUserItems = async (req, res) => {
  const userId = req.user._id;

  try {
    const userItems = await UserItem.find({ userId }).populate("itemId"); // Populates with item details if needed
    res.status(200).json({ items: userItems });
  } catch (error) {
    res
      .status(500)
      .json({ errors: { global: "An error occurred. - " + error.message } });
  }
};

// get user Item
export const getUserItem = async (req, res) => {
  const userId = req.user._id;
  const { itemId } = req.params;

  try {
    const userItem = await UserItem.findOne({ userId, itemId }).populate(
      "itemId"
    );

    if (!userItem) {
      return res
        .status(404)
        .json({ errors: { global: "Item not found for the user." } });
    }

    res.status(200).json(userItem);
  } catch (error) {
    res
      .status(500)
      .json({ errors: { global: "An error occurred. - " + error.message } });
  }
};

export default {
  addItemToUser,
  removeItemFromUser,
  getUserItems,
  getUserItem,
};
