import mongoose from "mongoose";

const userItemSchema = new mongoose.Schema(
  {
    userId: {
      type: mongoose.Schema.Types.ObjectId,
      required: true,
    },
    itemId: {
      type: mongoose.Schema.Types.ObjectId,
      ref: "Item", // Reference to the Item model
      required: true,
    },
    quantity: {
      type: Number,
      required: true,
      default: 1, // Default quantity if none specified
    },
  },
  { timestamps: true }
);

const UserItem = mongoose.model("UserItem", userItemSchema);

export default UserItem;
