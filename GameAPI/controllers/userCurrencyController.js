import Currency from "../models/currencyModel.js";
import UserCurrency from "../models/userCurrencyModel.js";

/**
 * Controller to add currency quantity to user inventory
 * @param req The request data
 * @param res The result of the request
 * @returns {Promise<*>}
 */
export const addCurrencyToUser = async (req, res) => {
  const { currencyId, quantity } = req.body;
  const userId = req.user._id;

  if (!currencyId || quantity <= 0) {
    return res
      .status(400)
      .json({ errors: { global: "Invalid currency ID or quantity." } });
  }

  try {
    // Retrieve the maximum quantity per slot and the maximum number of slots the currency can take.
    const currencyData = await Currency.findOne({ _id: currencyId }, [
      "maxQuantity",
    ]).exec();

    // Retrieve the existing number of currency.
    const userCurrency = await UserCurrency.findOne({ userId, currencyId }, [
      "quantity",
    ]).exec();
    let existingQuantity = 0;
    if (userCurrency) {
      existingQuantity = userCurrency.quantity;
    }

    // Calculate the number of currency that will not be added if the max number is reached.
    let remainingQuantity = 0;
    let addingQuantity = quantity;
    if (
      currencyData.maxQuantity > -1 &&
      existingQuantity + quantity > currencyData.maxQuantity
    ) {
      remainingQuantity =
        existingQuantity + quantity - currencyData.maxQuantity;
      addingQuantity -= remainingQuantity;
    }

    // Find or create the UserCurrency.
    const userCurrencyUpdated = await UserCurrency.findOneAndUpdate(
      { userId, currencyId },
      { $inc: { quantity: addingQuantity } },
      { new: true, upsert: true, setDefaultsOnInsert: true }
    );

    res.status(200).json({
      userCurrency: userCurrencyUpdated,
      remainingQuantity,
    });
  } catch (error) {
    res
      .status(500)
      .json({ errors: { global: "An error occurred. - " + error.message } });
  }
};

// Controller to remove Currency quantity from user inventory
export const removeCurrencyFromUser = async (req, res) => {
  const { currencyId, quantity } = req.body;
  const userId = req.user._id;

  if (!currencyId || quantity <= 0) {
    return res
      .status(400)
      .json({ errors: { global: "Invalid currency ID or quantity." } });
  }

  try {
    // Find the UserCurrency
    const userCurrency = await UserCurrency.findOne({ userId, currencyId });

    if (!userCurrency) {
      return res
        .status(404)
        .json({ errors: { global: "Currency not found for the user." } });
    }

    if (userCurrency.quantity < quantity) {
      return res
        .status(400)
        .json({ errors: { global: "Insufficient quantity." } });
    }

    userCurrency.quantity -= quantity;

    if (userCurrency.quantity === 0) {
      // Delete the currency if quantity reaches 0
      await userCurrency.deleteOne();
      return res
        .status(200)
        .json({ errors: { global: "Currency removed completely." } });
    } else {
      await userCurrency.save();
      res.status(200).json(userCurrency);
    }
  } catch (error) {
    res
      .status(500)
      .json({ errors: { global: "An error occurred. - " + error.message } });
  }
};

// get user Currencies
export const getUserCurrencies = async (req, res) => {
  const userId = req.user._id;

  try {
    const userCurrencies = await UserCurrency.find({ userId }).populate(
      "currencyId"
    ); // Populates with currency details if needed
    res.status(200).json({ currencies: userCurrencies });
  } catch (error) {
    res
      .status(500)
      .json({ errors: { global: "An error occurred. - " + error.message } });
  }
};

// get user Currency
export const getUserCurrency = async (req, res) => {
  const userId = req.user._id;
  const { currencyId } = req.params;

  try {
    const userCurrency = await UserCurrency.findOne({
      userId,
      currencyId,
    }).populate("currencyId");

    if (!userCurrency) {
      return res
        .status(404)
        .json({ errors: { global: "Currency not found for the user." } });
    }

    res.status(200).json(userCurrency);
  } catch (error) {
    res
      .status(500)
      .json({ errors: { global: "An error occurred. - " + error.message } });
  }
};

export default {
  addCurrencyToUser,
  removeCurrencyFromUser,
  getUserCurrencies,
  getUserCurrency,
};
