import * as React from 'react';
import { useEffect, useState } from 'react';
import Paper from '@mui/material/Paper';
import { ViewState } from '@devexpress/dx-react-scheduler';
import { useParams, useNavigate } from 'react-router-dom';
import axios from "axios";
import CircularProgress from '@mui/material/CircularProgress';
import {
    Scheduler,
    DayView,
    Appointments,
    AppointmentTooltip,
    Toolbar,
    DateNavigator,
    TodayButton,
    Resources
} from '@devexpress/dx-react-scheduler-material-ui';

const hourMap = [
    { hour: 0, startTime: 'T08:00', endTime:'T09:20'},
    { hour: 1, startTime: 'T09:40', endTime:'T11:00'},
    { hour: 2, startTime: 'T11:20', endTime:'T12:40'},
    { hour: 3, startTime: 'T13:00', endTime:'T14:20'},
    { hour: 4, startTime: 'T14:40', endTime:'T16:00'},
    { hour: 5, startTime: 'T16:20', endTime:'T17:40'}
]
const dayMap = [
    { day: 0, dayString: '2022-11-21'},
    { day: 1, dayString: '2022-11-22'},
    { day: 2, dayString: '2022-11-23'},
    { day: 3, dayString: '2022-11-24'},
    { day: 4, dayString: '2022-11-25'}
]

export function Schedule(props) {
    const params = useParams();
    const [loading, setLoading] = useState(true);
    const [schedulerData, setSchedulerData] = useState([]);
    const [resources, setResources] = useState([]);
    useEffect(() => {
        axios.get(`api/schedule/${params.id}`).then((response) => {
            setSchedulerData(response.data.map(l => {
                const dm = dayMap.find(dMap => dMap.day === l.day)
                const hm = hourMap.find(hMap => hMap.hour === l.hour)
                return { startDate: dm.dayString + hm.startTime, endDate: dm.dayString + hm.endTime, title: l.discipline, room: l.room, teacher: l.teacher }
            }));
            setResources(...resources, [{
                fieldName: 'room',
                title: 'Room',
                instances: [...new Set(response.data.map(l => ({ id: l.room, text: 'Аудиторія: ' +  l.room})))],
            },{
                fieldName: 'teacher',
                title: 'Teacher',
                instances: [...new Set(response.data.map(l => ({ id: l.teacher, text:'Викладач: ' + l.teacher })))],
            }])
            setLoading(false)
        })
    }, [])
    let contents = loading
        ? <CircularProgress />
        : <Paper sx={{mt:4} } >
            <Scheduler
                data={schedulerData}
            >
                <ViewState
                    defaultCurrentDate="2022-11-22"
                />
                <DayView
                    startDayHour={8}
                    endDayHour={18}
                />
                <Toolbar />
                <DateNavigator />
                <TodayButton />
                <Appointments />
                <AppointmentTooltip />
                <Resources
                    data={resources}
                />
            </Scheduler>
        </Paper>
    return (
        <div>
            {contents}
        </div>
    );
}
