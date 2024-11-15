import mongoose from "mongoose";

const userCurrencySchema = new mongoose.Schema(
  {
    userId: {
      type: mongoose.Schema.Types.ObjectId,
      required: true,
    },
    currencyId: {
      type: mongoose.Schema.Types.ObjectId,
      ref: "Currency", // Reference to the Currency model
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

const UserCurrency = mongoose.model("UserCurrency", userCurrencySchema);

export default UserCurrency;
