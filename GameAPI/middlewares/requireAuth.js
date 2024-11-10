const requireAuth = async (req, res, next) => {
  // verify authentification
  if (!req.user) {
    return res.status(401).json({ error: "request is not authorized" });
  }
  next();
};

export default requireAuth;
