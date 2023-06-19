import { Button, ButtonGroup, Container, Typography } from "@mui/material";

export default function AboutPage(){
    return(
        <Container>
        <Typography gutterBottom variant="h2">Errors for testing purposes</Typography>
       <ButtonGroup fullWidth>
        <Button variant='container' onClick={() => agent.TestErrors.get400Error().catch(error => console.log(error))}>Test 400 Error</Button>
        <Button variant='container' onClick={() => agent.TestErrors.get401Error().catch(error => console.log(error))}>Test 401 Error</Button>
        <Button variant='container' onClick={() => agent.TestErrors.get404Error().catch(error => console.log(error))}>Test 404 Error</Button>
        <Button variant='container' onClick={() => agent.TestErrors.get500Error().catch(error => console.log(error))}>Test 500 Error</Button>
        <Button variant='container' onClick={() => agent.TestErrors.getValidationError().catch(error => console.log(error))}>Test Validation Error</Button>
       </ButtonGroup>
        </Container>
    )
}