import mongoose from "mongoose";

const itemSchema = new mongoose.Schema(
  {
    name: {
      type: String,
      required: true,
    },
    description: {
      type: String,
      required: true,
    },
    type: {
      type: String,
      enum: [
        "none",
        "weapons",
        "equipments",
        "ingredients",
        "meals",
        "ressources",
        "consumables",
        "quests",
      ],
      required: true,
      default: "none",
    },

    rarity: {
      type: String,
      enum: ["common", "rare", "epic", "legendary"],
      required: true,
      default: "common",
    },
    maxQuantityPerSlot: {
      type: Number,
      required: true,
      default: -1,
    },
    maxSlot: {
      type: Number,
      required: true,
      default: -1,
    },
  },
  { timestamps: true }
);

const Item = mongoose.model("Item", itemSchema);

export default Item;
