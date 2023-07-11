# install.packages("dplyr", dep=TRUE, rep= "http://cran.uk.r-project.org")
# install.packages("maditr", dep=TRUE, rep= "http://cran.uk.r-project.org")
suppressMessages(library(dplyr))
suppressMessages(library(maditr))

events_csv = read.csv('Events.csv',
            header = TRUE, sep = ",", quote = "\"",dec = ".",
            fill = TRUE, comment.char = "")
print(colnames(events_csv))

events_csv$companion_Type[events_csv$companion_Type=='0'] <- 'CompSC' 
events_csv$companion_Type[events_csv$companion_Type=='1'] <- 'CompSF' 
events_csv$companion_Type[events_csv$companion_Type=='2'] <- 'CompOC' 
events_csv$companion_Type[events_csv$companion_Type=='3'] <- 'CompOF' 

resultsLog <- events_csv %>% 
    group_by(study_ID,companion_Type,event_Type,event_Actuator,event_Receiver) %>% 
    summarise(numEvents = n()) 

finalEvents <- events_csv[events_csv$event_Type=='FINAL',]
finalEvents <- dcast(finalEvents, study_ID ~ companion_Type+event_Type, value.var = "score")
print(finalEvents)
# merge(x=resultsLog,y=finalEvents,by=c('study_ID','companion_Type'))


# print(head(finalEvents,5))
# q()
print(resultsLog)

resultsLog <- dcast(resultsLog, study_ID ~ companion_Type+event_Type+event_Actuator+event_Receiver, value.var="numEvents")

#MERGE FINAL SCORES WITH "EVENTS_PROCESSED.CSV"

print(resultsLog)

write.csv(resultsLog, 'Events_processed.csv')