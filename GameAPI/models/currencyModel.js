import mongoose from "mongoose";

const currencySchema = new mongoose.Schema(
  {
    name: {
      type: String,
      required: true,
    },
    description: {
      type: String,
      required: true,
    },
    maxQuantity: {
      type: Number,
      required: true,
      default: -1,
    },
  },
  { timestamps: true }
);

const Currency = mongoose.model("Currency", currencySchema);

export default Currency;
