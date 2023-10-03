import React, { useState } from "react";
import "./App.css";
import Login from "./Screens/Login";
import Dashboard from "./Screens/Dashboard";

function App() {
  const [token, setToken] = useState("");

  return (
    <div className="App">
      {!token ? <Login setToken={setToken} /> : <Dashboard token={token} />}
    </div>
  );
}

export default App;
