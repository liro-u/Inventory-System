const tryAddAuthWithUserAPI = async (req, res, next) => {
  const { authorization } = req.headers;
  req.user = null;

  if (!authorization) {
    return next(); // No token, proceed without authentication
  }

  try {
    const response = await fetch(
      process.env.USER_API_PROXY + "/api/user/tryAddAuth",
      {
        headers: { Authorization: authorization },
      }
    );

    if (response.ok) {
      const data = await response.json();
      req.user = data.user;
    } else if (response.status === 401) {
      return res.status(401).json({ error: "Session expired" });
    } else if (response.status === 404) {
      return res.status(404).json({ error: "User not found" });
    } else {
      console.error(`User API error: ${response.statusText}`);
    }
  } catch (error) {
    console.error(`Error fetching user data: ${error.message}`);
  }

  next();
};

export default tryAddAuthWithUserAPI;
