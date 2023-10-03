import React, { useState } from "react";
import { loginhelper } from "../apiHelpers/Loginhelper";

const Login = ({ setToken }) => {
  const [Username, setUsername] = useState("");
  const [Password, setPassword] = useState("");

  const handleLogin = async () => {
    try {
      const token = await loginhelper(Username, Password);
      setToken(token);
    } catch (error) {
      console.error("Login failed", error);
    }
  };

  return (
    <div className="login-container">
      <h2>Admin Login</h2>
      <input
        type="text"
        placeholder="Username"
        onChange={(e) => setUsername(e.target.value)}
      />
      <input
        type="password"
        placeholder="Password"
        onChange={(e) => setPassword(e.target.value)}
      />
      <button onClick={handleLogin}>Login</button>
    </div>
  );
};

export default Login;
