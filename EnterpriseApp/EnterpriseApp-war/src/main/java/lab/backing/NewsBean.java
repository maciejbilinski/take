package lab.backing;

import jakarta.annotation.Resource;
import jakarta.enterprise.context.RequestScoped;
import jakarta.inject.Inject;
import jakarta.inject.Named;
import jakarta.jms.JMSContext;
import jakarta.jms.JMSException;
import jakarta.jms.ObjectMessage;
import jakarta.jms.TextMessage;
import lab.ejb.NewsItem;
import lab.ejb.NewsItemFacadeLocal;

import java.util.List;

@RequestScoped
@Named
public class NewsBean {

    private String headingText;
    private String bodyText;

    @Inject
    private NewsItemFacadeLocal facade;

    @Inject
    private JMSContext context;

    @Resource(lookup="java:app/jms/NewsQueue")
    private jakarta.jms.Queue queue;

    void sendNewsItem(String heading, String body) {
        TextMessage message = context.createTextMessage(heading + "|" + body);
        context.createProducer().send(queue, message);
    }

    public List<NewsItem> getNewsItems(){
        return facade.getAllNewsItems();
    }

    public String getHeadingText() {
        return headingText;
    }

    public void setHeadingText(String headingText) {
        this.headingText = headingText;
    }

    public String getBodyText() {
        return bodyText;
    }

    public void setBodyText(String bodyText) {
        this.bodyText = bodyText;
    }

    public String submitNews()
    {
        sendNewsItem(headingText, bodyText);
        return null;
    }
}
