import * as React from 'react';
import { useEffect, useState } from 'react';
import Paper from '@mui/material/Paper';
import { ViewState, GroupingState, IntegratedGrouping } from '@devexpress/dx-react-scheduler';
import { useParams } from 'react-router-dom';
import axios from './AxiosInterceptor'
import CircularProgress from '@mui/material/CircularProgress';
import {
    Scheduler,
    DayView,
    Appointments,
    AppointmentTooltip,
    Toolbar,
    DateNavigator,
    TodayButton,
    Resources,
    GroupingPanel
} from '@devexpress/dx-react-scheduler-material-ui';
import { Button } from '@mui/material';
import CenteredCircularProgress from './CenteredCircularProgress';

const hourMap = [
    { hour: 0, startTime: 'T08:00', endTime: 'T09:20' },
    { hour: 1, startTime: 'T09:40', endTime: 'T11:00' },
    { hour: 2, startTime: 'T11:20', endTime: 'T12:40' },
    { hour: 3, startTime: 'T13:00', endTime: 'T14:20' },
    { hour: 4, startTime: 'T14:40', endTime: 'T16:00' },
    { hour: 5, startTime: 'T16:20', endTime: 'T17:40' }
]
const dayMap = [
    { day: 0, dayString: '2022-11-21' },
    { day: 1, dayString: '2022-11-22' },
    { day: 2, dayString: '2022-11-23' },
    { day: 3, dayString: '2022-11-24' },
    { day: 4, dayString: '2022-11-25' }
]

export function Schedule(props) {
    const params = useParams();
    const [loading, setLoading] = useState(true);
    const [schedulerData, setSchedulerData] = useState([]);
    const [resources, setResources] = useState([]);
    const [grouping, setGrouping] = useState([]);
    const [currentScheduleButtonState, setCurrentScheduleButtonState] = useState({ isDisabled: false, text: 'Зробити поточним' });
    const makeScheduleCurrent = () => {
        axios.put(`api/schedules/${params.id}`).then(response => {
            setCurrentScheduleButtonState({ isDisabled: true, text: 'Це поточний розклад' })
        })
    }
    useEffect(() => {
        axios.get(`api/schedules/${params.id}/lessons`).then((response) => {
            if(response.data.isCurrent){
                setCurrentScheduleButtonState({ isDisabled: true, text: 'Це поточний розклад' })
            }
            setSchedulerData(response.data.lessons.map(l => {
                const dm = dayMap.find(dMap => dMap.day === l.day)
                const hm = hourMap.find(hMap => hMap.hour === l.hour)
                return { startDate: dm.dayString + hm.startTime, endDate: dm.dayString + hm.endTime, title: l.discipline, room: l.room, teacher: l.teacher, group: l.group, rRule: 'FREQ=WEEKLY' }
            }));
            setResources(...resources, [{
                fieldName: 'room',
                title: 'Room',
                instances: [...new Set(response.data.lessons.map(l => l.room))].map(room => ({ id: room, text: 'Аудиторія: ' + room })),
            },
            {
                fieldName: 'teacher',
                title: 'Teacher',
                instances: [...new Set(response.data.lessons.map(l => l.teacher))].map(teacher => ({ id: teacher, text: 'Викладач: ' + teacher })),
            },
            {
                fieldName: 'group',
                title: 'Group',
                instances: [...new Set(response.data.lessons.map(l => l.group))].map(group => ({ id: group, text: 'Група: ' + group }))
            }]);
            setGrouping([{
                resourceName: 'group'
            }])
            setLoading(false)
        })
    }, [])
    let contents = loading
        ? <CenteredCircularProgress />
        : <Paper sx={{ mt: 4 }} >
            <Button hidden={localStorage.getItem("username") !== "admin"} variant="contained" disabled={currentScheduleButtonState.isDisabled} onClick={makeScheduleCurrent}>{currentScheduleButtonState.text}</Button>
            <Scheduler
                data={schedulerData}
                locale={'ua-UA'}
            >
                <ViewState />
                <GroupingState
                    grouping={grouping}
                />
                <DayView
                    startDayHour={8}
                    endDayHour={18}
                />
                <Appointments />
                <Resources
                    data={resources}
                    mainResourceName="group"
                />

                <IntegratedGrouping />

                <Toolbar />
                <DateNavigator />
                <TodayButton messages={{today:'Сьогодні'}} />
                <AppointmentTooltip />
                <GroupingPanel />
            </Scheduler>
        </Paper>
    return (
        <div>
            {contents}
        </div>
    );
}
