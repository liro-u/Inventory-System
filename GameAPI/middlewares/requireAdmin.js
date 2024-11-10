const requireAdmin = (req, res, next) => {
  // verify authentification
  if (!req.user) {
    return res.status(401).json({ error: "request is not authorized" });
  }

  if (!req.user.isAdmin) {
    return res.status(401).json({ error: "request is not authorized" });
  }
  next();
};

export default requireAdmin;
