
import Catalog from "../../features/catalog/Catalog";
import {Container, createTheme, CssBaseline, ThemeProvider} from "@mui/material";
import Header from "./Header";
import { useEffect, useState } from "react";
import { Outlet } from "react-router-dom";
import { ToastContainer } from "react-toastify";
import 'react-toastify/dist/ReactToastify.css';
import { useStoreContext } from "../context/StoreContext";
import { getCookie } from "../util/util";
import agent from "../api/agent";
import LoadingComponent from "./LoadingComponent";
import { useAppDispatch } from "../store/configureStore";
import { setBasket } from "../../features/basket/BasketSlice";

function App() {
  const dispatch = useAppDispatch();
  const [loading, setLoading] = useState(true);

  //Getting basket based on cookie
  //getting basket from api
  //catching error, turn of loading
  useEffect(() => {
    const buyerId = getCookie('buyerId');
    if (buyerId){
      agent.Basket.get()
      .then(basket => dispatch(setBasket(basket)))
      .catch(error => console.log(error))
      .finally(() => setLoading(false));
    }else{
      setLoading(false);
    }
  }, [dispatch])

  const [darkMode, setDarkMode] = useState(false);
  const paletteType = darkMode ? 'dark' : 'light';
 const theme = createTheme({
  palette:{
    mode: paletteType,
    background: {
       default: paletteType === 'light' ? '#ffffff' : '#121212'
}
  }
 })

 //Toggle for darkmode
 function handleThemeChange(){
  setDarkMode(!darkMode);
 }

 if (loading) return <LoadingComponent message="Initializing app..."/>

  return (
    <ThemeProvider theme={theme}>
      <ToastContainer position="bottom-right" hideProgressBar theme="colored"/>
    <CssBaseline/> {/*Removes margins on navbar*/}
      <Header darkMode={darkMode} handleThemeChange={handleThemeChange}/>
      <Container>
   <Outlet/>
    </Container>
    </ThemeProvider>
  );
}

export default App;
