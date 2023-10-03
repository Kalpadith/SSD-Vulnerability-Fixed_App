import React, { useEffect, useState } from "react";
import "../Styles/DashboardStyles.css";
import Table from "../components/Dashboard_Components/Table";
import { fetchUserData } from "../apiHelpers/Dahboardhelper";

const Dashboard = ({ token }) => {
  const [userData, setUserData] = useState([]);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const data = await fetchUserData(token);

        setUserData(data);
      } catch (error) {
        console.error("Error fetching data", error);
      }
    };

    fetchData();
  }, [token]);

  return (
    <div className="dashboard-container">
      <h2>User Data Dashboard</h2>
      <Table userData={userData} />
    </div>
  );
};

export default Dashboard;
