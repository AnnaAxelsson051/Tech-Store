
import {Container, createTheme, CssBaseline, ThemeProvider} from "@mui/material";
import Header from "./Header";
import { useCallback, useEffect, useState } from "react";
import { Outlet } from "react-router-dom";
import { ToastContainer } from "react-toastify";
import 'react-toastify/dist/ReactToastify.css';
import LoadingComponent from "./LoadingComponent";
import { useAppDispatch } from "../store/configureStore";
import { fetchBasketAsync } from "../../features/basket/BasketSlice";
import { fetchCurrentUser } from "../../features/account/accountSlice";


function App() {
  const dispatch = useAppDispatch();
  const [loading, setLoading] = useState(true);

  const initApp = useCallback(async () => {
    try{
      await dispatch(fetchCurrentUser());
      await dispatch(fetchBasketAsync());
    } catch (error) {
    console.log(error);  
    }
  }, [dispatch])


  //Getting basket based on cookie
  //getting basket from api
  //catching error, turn of loading
  useEffect(() => {
initApp().then(() => setLoading(false));
  }, [initApp])

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
