import { CircularProgress } from "@mui/material";

export default function CenteredCircularProgress(props) {
    return (<CircularProgress sx={{
        position: 'absolute',
        top: '50%',
        left: '50%'
    }} />);
}